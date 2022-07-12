Shader "Instanced/InstancedShader" {
    Properties {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _CutOut ("cut out", float) =0.2
        _WiggleSize ("Wiggle Size" , float) = 0.2
    }
    SubShader {

        Pass {

            //Tags {"LightMode"="ForwardBase"}

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase nolightmap nodirlightmap nodynlightmap novertexlight
            #pragma target 4.5

            #include "UnityCG.cginc"
            #include "UnityLightingCommon.cginc"
            #include "AutoLight.cginc"

            float _CutOut;
            float _WiggleSize;
            sampler2D _MainTex;
            struct Point
            {
                float3 position;
                float3 rotation;
                float3 scale;
            };

            StructuredBuffer<Point> _PointsBuffer;
            

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv_MainTex : TEXCOORD0;
                float3 ambient : TEXCOORD1;
                float3 diffuse : TEXCOORD2;
                float3 color : TEXCOORD3;
                SHADOW_COORDS(4)
            };

            void rotate2D(inout float2 v, float r)
            {
                float s, c;
                sincos(radians(r)  , s, c);
                v = float2(v.x * c - v.y * s, v.x * s + v.y * c);
            }

            v2f vert (appdata_full v, uint inst : SV_InstanceID)
            {
                v2f o;
                Point _p =_PointsBuffer[inst];
                rotate2D(_p.position.xz, _p.rotation);

                float3 worldNormal = v.normal;
                float3 worldPosition=v.vertex *_p.scale +_p.position;
                v.vertex += cos(sin(_Time+length( worldPosition)))*_WiggleSize ;
                
                float3 ndotl = saturate(dot(worldNormal, _WorldSpaceLightPos0.xyz));
                float3 ambient = ShadeSH9(float4(worldNormal, 1.0f));
                float3 diffuse = (ndotl * _LightColor0.rgb);
                float3 color = v.color;

                //o.pos = mul(UNITY_MATRIX_VP, float4(worldPosition, 1.0f));
                o.pos= UnityObjectToClipPos((v.vertex + worldPosition) );
                o.uv_MainTex = v.texcoord ;
                o.ambient = ambient;
                o.diffuse = diffuse;
                o.color = color;
                TRANSFER_SHADOW(o)
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed shadow = SHADOW_ATTENUATION(i);
                fixed4 albedo = tex2D(_MainTex, i.uv_MainTex);

                clip(albedo.a -_CutOut );

                float3 lighting = i.diffuse * shadow + i.ambient;
                fixed4 output = fixed4(albedo.rgb * i.color * lighting, albedo.w);
                UNITY_APPLY_FOG(i.fogCoord, output);
                return output;
            }

            ENDCG
        }
    }
}