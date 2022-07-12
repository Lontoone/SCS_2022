Shader "Unlit/WiggleEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
         _Tint ("Tint", Color) = (1,1,1,1)

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
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
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            float4 _MainTex_ST;
            float4 _Tint;

            sampler2D _MainTex;

            v2f vert (appdata v)
            {
                v2f o;
                float3 worldPos = mul (unity_ObjectToWorld, v.vertex).xyz;

                v.vertex += cos(sin(_Time+length( worldPos)));

                o.vertex = UnityObjectToClipPos(v.vertex );
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv ) *_Tint;
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
        /*
        //Additional Pass
        Pass
        {
            Blend One One
            Tags{ "LightMode" = "LightweightForward" }
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #include "AutoLight.cginc"
            #include "UnityLightingCommon.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;

            sampler2D _AlphaTex;
            float4 _AlphaTex_ST;
            
            struct v2f
            {
                float4 pos : SV_POSITION;
                LIGHTING_COORDS(0, 1)
                float2 uv : TEXCOORD2;
                float2 mask_uv : TEXCOORD3;
                float3 lightDir : TEXCOORD4;
                float3 viewDir : TEXCOORD5;
                float3 normal : NORMAL;
            };
            
            
            v2f vert(appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                o.lightDir = normalize(ObjSpaceLightDir(v.vertex));
                o.mask_uv = TRANSFORM_TEX(v.texcoord, _AlphaTex);
                o.normal = normalize(v.normal).xyz;
                TRANSFER_VERTEX_TO_FRAGMENT(o);
                
                return o;
            }
            
            fixed4 frag(v2f i) : COLOR
            {
                float4 alphaMask = tex2D(_AlphaTex, i.mask_uv);
                clip(alphaMask.a - 0.1);
                
                float attenuation = LIGHT_ATTENUATION(i);
                float4 ambient = UNITY_LIGHTMODEL_AMBIENT;
                
                float4 albedo = tex2D(_MainTex, i.uv);
                return  attenuation*_LightColor0*ambient;
                
            }
            ENDCG

        }
    }*/
}
}