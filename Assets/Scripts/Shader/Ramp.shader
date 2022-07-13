// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/Ramp"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("color tint" ,Color)=(1,1,1,1)
        _RampTex("Ramp tex",2D)="white"{}
        _Specular ("Specular",Color)=(1,1,1,1)
        _Gloss ("Gloss",Range(8,256))=20
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
            #pragma multi_compile_instancing
             
            #pragma multi_compile_fog

            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            // *** 變數 ***
            sampler2D _MainTex;
            float4 _MainTex_ST; 
            sampler2D _RampTex;
            float4 _RampTex_ST;
            fixed4 _Specular;
            float _Gloss;
            float4 _Color;


            //  *** 結構 ***
            struct a2v{
                float4 vertex :POSITION;
                float3 normal :NORMAL;
                float2 texcoord :TEXCOORD0;
            };
            struct v2f{
                float4 pos:SV_POSITION;
                float3 worldNormal:TEXCOORD0;
                float3 worldPos:TEXCOORD1;
                float2 uv:TEXCOORD2;
                
                UNITY_FOG_COORDS(3)
            };

            v2f vert (a2v v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldNormal=UnityObjectToWorldNormal(v.normal);
                o.worldPos=mul(unity_ObjectToWorld,v.vertex).xyz;                
                o.uv = TRANSFORM_TEX(v.texcoord, _RampTex);
                
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float4 color = tex2D(_MainTex, i.uv);
                fixed3 worldNormal=normalize(i.worldNormal);                
                fixed3 worldLightDir=normalize(UnityWorldSpaceLightDir(i.worldPos));      
                fixed3 ambient=UNITY_LIGHTMODEL_AMBIENT.xyz;
                //半伯蘭特模型
                fixed halfLambert =0.5*dot(worldNormal,worldLightDir)+0.5; 
                //將漸層貼圖映射在 0~1之間               
                fixed3 diffuseColor=tex2D(_RampTex , fixed2(halfLambert,halfLambert)).rgb * _Color.rgb;
                fixed3 diffuse=_LightColor0.rgb * diffuseColor;
                
                fixed3 viewDir=normalize(UnityWorldSpaceViewDir(i.worldPos));
                fixed3 halfDir=normalize(worldLightDir+viewDir);
                fixed3 specular = _LightColor0.rgb * _Specular.rgb*pow(max(0,dot(worldNormal,halfDir)),_Gloss);

                float4 result =float4(color + ambient+diffuse+specular,1);    
                UNITY_APPLY_FOG(i.fogCoord, result);
                return  result;
            }
            ENDCG
        }
    }    
    Fallback "Specular"
}