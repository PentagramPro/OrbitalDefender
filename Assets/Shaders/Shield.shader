Shader "Custom/Shield" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Phase ("Animation phase", Float) = 5
		_Period ("Animation period", Float) = 5
		
	}
   SubShader {
      Tags { "Queue" = "Transparent" } 
         // draw after all opaque geometry has been drawn
      Pass {
         ZWrite Off // don't write to depth buffer 
            // in order not to occlude other objects
 
         Blend SrcAlpha One // use alpha blending
 			
         CGPROGRAM 
 
         #pragma vertex vert 
         #pragma fragment frag
 
 		uniform sampler2D _MainTex;	
 		uniform float _Phase;
 		uniform float _Period;
 		
 		struct vertexInput {
            float4 vertex : POSITION;
            float4 texcoord : TEXCOORD0;
         };
         struct vertexOutput {
            float4 pos : SV_POSITION;
            float4 tex : TEXCOORD0;
         };
         
         vertexOutput vert(vertexInput input) 
         {
            vertexOutput output;
 
            output.tex = input.texcoord;

            output.pos = mul(UNITY_MATRIX_MVP, input.vertex);
            return output;
         }
 
         float4 frag(vertexOutput input) : COLOR 
         {
         	float2 tex;
         	float2 basePhase = _Phase+_Period*input.tex.xy;
         	tex.x = input.tex.x+0.002f*sin(basePhase.x)+0.001f*sin(2*basePhase.y);
         	tex.y = input.tex.y+0.002f*sin(basePhase.y)+0.001f*sin(2*basePhase.x);
            float4 col = tex2D(_MainTex, tex);
            //float4 alpha = tex2D(_MainTex, input.tex.xy);
            
            //col.a = alpha.a;
            
            //col = col*0.5f;
            
            return col;
               
         }
 
         ENDCG  
      }
   }
}