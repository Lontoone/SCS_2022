Shader "Unlit/CameraOverLapImage"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" { }//攝影機畫面
        _DisplaceTexture ("Displace Texture", 2D) = "white" { }// 扭曲
        _MaskTex ("Mask texture", 2D) = "white" { } //遮罩
        _OverLapImage ("Over Lap image", 2D) = "white" { } //要覆蓋畫面的圖片
        _SoftClipRange ("Soft CLip Range" , float) = 5
        //_RangeSize ("Range" , float) = 0.2

    }
    SubShader
    {
        //Tags { "RenderType"="Opaque" }
        Cull off ZWrite off ZTest Always
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                //UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _DisplaceTexture;
            sampler2D _OverLapImage;
            sampler2D _MaskTex;
            float4 _MainTex_ST;
            float _Magnitude;
            float _MaskTex_mag; //0~1 mask 顯示程度
            float _SoftClipRange;
            float _RangeSize;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                //=>o.uv = v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float4 color = tex2D(_MainTex, i.uv );
                float distance = length( i.uv - float2(0.5,0.5));
                float _isInrange = distance < (_RangeSize + _SoftClipRange) ;
                float _t = clamp( ( distance - _RangeSize )/_SoftClipRange,0,1);
                float mask = lerp(  0,1, _t );
                float4 maskedMainTex = color * (1-mask) * _isInrange;

                float4 overlapImg = tex2D(_OverLapImage, i.uv );
                float4 result = lerp(maskedMainTex, overlapImg, Luminance(overlapImg));

                return result;
                
                /*
                //位移
                float2 dis = tex2D(_DisplaceTexture, i.uv + _Time.x).xy;
                dis = ((dis * 2) - 1) * _Magnitude;  //(*2-1) -> 把uv範圍[0,1] 改成 [-1,1]

                //算遮罩
                float4 mask = tex2D(_MaskTex, i.uv);
                //float4 overlapImg = tex2D(_OverLapImage, i.uv + dis);
                float4 overlapImg = tex2D(_OverLapImage, i.uv );
                float4 effect = mask * overlapImg.a * _MaskTex_mag;
                float4 color = tex2D(_MainTex, i.uv );
                float4 result = lerp(color, overlapImg, 1 - Luminance(overlapImg));
                //float4 output       = color - effect ;
                float4 output = result - effect ;

                //return output;
                return color - (1-mask) *_MaskTex_mag ;*/
            }

            
            ENDCG
        }
    }
}
