

Shader "CelShading" {
    Properties{
        _Color("Color", Color) = (1, 1, 1)
		_MainTex("Albedo (RGB)", 2D) = "white" {
            }
		
		_Dim("Dim", Color) = (1,1,1)
		_MaxBrightness("Top",Float) = 1
		_Attenuation("Lit",Range(0,1.0)) = 0.8
			_Attenuation2("Lit2",Range(0,1.0)) = 0.6
		_Range("Range",Range(-1.0, 1.0)) = 0.6
			_Range2("RangeSecond",Range(-1.0, 1.0)) = 0.1
}
		SubShader{
        Tags{
            //"Queue" = "Transparent"
			"RenderType" = "Opaque"
		}
			LOD 200
			CGPROGRAM

	#include "UnityCG.cginc"
	#include "UnityStandardConfig.cginc"
	#include "UnityLightingCommon.cginc"
			//<3 <3
	#pragma surface surf CelShadingForward fullforwardshadows 
	#pragma target 3.0
			
		float _Range;
		float _Range2;
		float _Attenuation;
		float _Attenuation2;
        half4 LightingCelShadingForward(SurfaceOutput s, half3 lightDir, half atten) {
			 
			half NdotL = (dot(s.Normal, lightDir)) * atten;
			half4 c;
			if (NdotL <= _Range&&NdotL>_Range2){ NdotL = _Attenuation;
					
			}
			else if(NdotL<=_Range2){ NdotL = _Attenuation2; }
			else NdotL = 1;
					
            if (s.Alpha < 0.01)discard;
			c.rgb =s.Albedo*(NdotL)*_LightColor0.rgb;;
			c.a = s.Alpha;
            return c;
        }

		half3 _Dim;
		sampler2D _MainTex;
        fixed4 _Color;
        struct Input {
            float2 uv_MainTex;
        };
	

        void surf(Input IN, inout SurfaceOutput o) {
            // Albedo comes from a texture tinted by color
			
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex)*_Color ;
            if (c.a < 0.1)discard;
            o.Albedo = c.rgb*_Dim;
            o.Alpha = c.a;
        }


		ENDCG

	}
		FallBack "VertexLit"
}