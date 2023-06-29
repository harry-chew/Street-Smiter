// Made with Amplify Shader Editor v1.9.1.5
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Clouds"
{
	Properties
	{
		_Noise("Noise", 2D) = "white" {}
		_CloudCutoff("Cloud Cutoff", Range( 0 , 1)) = 0.3101562
		_CloudSoftness("Cloud Softness", Range( 0 , 3)) = 1.489919
		_NoiseMainScale("Noise Main Scale", Float) = 0.5
		_Noise2Scale("Noise 2 Scale", Float) = 0.5
		_Noise1Scale("Noise 1 Scale", Float) = 0.5
		_NoiseSpeed("Noise Speed", Float) = 0.25
		_midYValue("midYValue", Float) = 0.5
		_CloudHeight("Cloud Height", Float) = 0
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Float0("Float 0", Float) = 0.85
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Geometry+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float3 worldPos;
			float2 uv_texcoord;
		};

		uniform float _midYValue;
		uniform float _CloudHeight;
		uniform float _Float0;
		uniform sampler2D _Noise;
		uniform float _Noise1Scale;
		uniform float _NoiseSpeed;
		uniform float _NoiseMainScale;
		uniform float _Noise2Scale;
		uniform sampler2D _TextureSample0;
		uniform float4 _TextureSample0_ST;
		uniform float _CloudCutoff;
		uniform float _CloudSoftness;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float3 ase_worldPos = i.worldPos;
			float temp_output_139_0 = ( 1.0 - pow( saturate( ( abs( ( _midYValue - ase_worldPos.y ) ) / ( _CloudHeight * 0.0 ) ) ) , _Float0 ) );
			float4 temp_cast_0 = (temp_output_139_0).xxxx;
			float2 appendResult6 = (float2(ase_worldPos.x , ase_worldPos.z));
			float mulTime15 = _Time.y * _NoiseSpeed;
			float2 appendResult13 = (float2(mulTime15 , mulTime15));
			float4 temp_output_30_0 = ( tex2D( _Noise, ( _Noise1Scale * ( appendResult6 - appendResult13 ) * _NoiseMainScale ) ) * tex2D( _Noise, ( ( appendResult6 + appendResult13 ) * _Noise2Scale * _NoiseMainScale ) ) );
			o.Emission = ( 1.0 - ( temp_cast_0 - ( temp_output_30_0 * temp_output_139_0 ) ) ).rgb;
			float2 uv_TextureSample0 = i.uv_texcoord * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
			float4 temp_cast_2 = (_CloudCutoff).xxxx;
			float4 temp_cast_3 = (_CloudSoftness).xxxx;
			o.Alpha = pow( saturate( (float4( 0,0,0,0 ) + (( temp_output_30_0 * temp_output_139_0 * tex2D( _TextureSample0, uv_TextureSample0 ).r ) - temp_cast_2) * (float4( 1,1,1,1 ) - float4( 0,0,0,0 )) / (float4( 1,1,1,1 ) - temp_cast_2)) ) , temp_cast_3 ).r;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha fullforwardshadows exclude_path:deferred 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=19105
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;109;874.8997,-176.8001;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Clouds;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;;0;False;;False;0;False;;0;False;;False;0;Custom;0.5;True;True;0;True;TransparentCutout;;Geometry;ForwardOnly;12;all;True;True;True;True;0;False;;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;2;15;10;25;False;0.5;True;0;5;False;;10;False;;0;0;False;;0;False;;0;False;;0;False;;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;6;-1;-1;-1;0;False;0;0;False;;-1;0;False;;0;0;0;False;0.1;False;;0;False;;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;30;-113.9735,-0.5382977;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TFHCRemapNode;31;286.4257,-16.13828;Inherit;False;5;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,1;False;3;COLOR;0,0,0,0;False;4;COLOR;1,1,1,1;False;1;COLOR;0
Node;AmplifyShaderEditor.WorldPosInputsNode;7;-1614.557,23.39864;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DynamicAppendNode;13;-1351.593,273.094;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;6;-1346.355,110.3986;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;12;-1143.594,150.8937;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleTimeNode;15;-1598.595,289.994;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;120;-710.553,221.8987;Inherit;False;3;3;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;116;-689.1591,-175.708;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;122;68.6123,20.42096;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;126;-1795.967,289.1595;Float;False;Property;_NoiseSpeed;Noise Speed;7;0;Create;True;0;0;0;False;0;False;0.25;0.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;28;-912.8765,77.91764;Float;False;Property;_NoiseMainScale;Noise Main Scale;3;0;Create;True;0;0;0;False;0;False;0.5;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;115;-928.0771,403.7632;Float;False;Property;_Noise2Scale;Noise 2 Scale;4;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;114;-904.3788,-300.5678;Float;False;Property;_Noise1Scale;Noise 1 Scale;5;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;127;-1374.475,791.1622;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleSubtractOpNode;129;-1142.728,746.8611;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;130;-955.7806,750.1224;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;131;-786.6115,751.8742;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;132;-933.6624,898.6476;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;135;-464.6723,781.3738;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;133;-651.7077,751.2783;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;139;-289.7061,779.4517;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;137;77.85143,544.7715;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;138;220.374,550.1292;Inherit;False;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;140;376.8284,568.3473;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TexturePropertyNode;119;-972.6524,-530.5225;Inherit;True;Property;_Noise;Noise;0;0;Create;True;0;0;0;False;0;False;f63f2d375ed2a2843889377612053517;f63f2d375ed2a2843889377612053517;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.SamplerNode;141;-849.1716,498.2565;Inherit;True;Property;_TextureSample0;Texture Sample 0;10;0;Create;True;0;0;0;False;0;False;-1;4b57a6210b522ed42bc1397bc0a5a4d9;4b57a6210b522ed42bc1397bc0a5a4d9;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;124;-491.2485,157.7111;Inherit;True;Property;_Noise2;Noise 2;3;0;Create;True;0;0;0;False;0;False;-1;f63f2d375ed2a2843889377612053517;f63f2d375ed2a2843889377612053517;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;38;-0.04683828,-191.1063;Float;False;Property;_CloudSoftness;Cloud Softness;2;0;Create;True;0;0;0;False;0;False;1.489919;1.489919;0;3;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;33;-375.2748,377.7617;Float;False;Property;_CloudCutoff;Cloud Cutoff;1;0;Create;True;0;0;0;False;0;False;0.3101562;0.3101562;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;36;488.2112,-13.2497;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.PowerNode;37;629.9112,-14.54941;Inherit;False;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;136;-657.6915,873.5807;Float;False;Property;_Float0;Float 0;11;0;Create;True;0;0;0;False;0;False;0.85;0.85;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;134;-1132.946,952.2852;Float;False;Property;_CloudHeight;Cloud Height;9;0;Create;True;0;0;0;False;0;False;0;0.38;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;128;-1351.402,692.0012;Float;False;Property;_midYValue;midYValue;8;0;Create;True;0;0;0;False;0;False;0.5;0.85;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;110;-1146.663,38.89832;Inherit;False;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;123;-487.6819,-312.9982;Inherit;True;Property;_Noise1;Noise 1;4;0;Create;True;0;0;0;False;0;False;-1;f63f2d375ed2a2843889377612053517;f63f2d375ed2a2843889377612053517;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
WireConnection;109;2;140;0
WireConnection;109;9;37;0
WireConnection;30;0;123;0
WireConnection;30;1;124;0
WireConnection;31;0;122;0
WireConnection;31;1;33;0
WireConnection;13;0;15;0
WireConnection;13;1;15;0
WireConnection;6;0;7;1
WireConnection;6;1;7;3
WireConnection;12;0;6;0
WireConnection;12;1;13;0
WireConnection;15;0;126;0
WireConnection;120;0;12;0
WireConnection;120;1;115;0
WireConnection;120;2;28;0
WireConnection;116;0;114;0
WireConnection;116;1;110;0
WireConnection;116;2;28;0
WireConnection;122;0;30;0
WireConnection;122;1;139;0
WireConnection;122;2;141;1
WireConnection;129;0;128;0
WireConnection;129;1;127;2
WireConnection;130;0;129;0
WireConnection;131;0;130;0
WireConnection;131;1;132;0
WireConnection;132;0;134;0
WireConnection;135;0;133;0
WireConnection;135;1;136;0
WireConnection;133;0;131;0
WireConnection;139;0;135;0
WireConnection;137;0;30;0
WireConnection;137;1;139;0
WireConnection;138;0;139;0
WireConnection;138;1;137;0
WireConnection;140;0;138;0
WireConnection;124;0;119;0
WireConnection;124;1;120;0
WireConnection;36;0;31;0
WireConnection;37;0;36;0
WireConnection;37;1;38;0
WireConnection;110;0;6;0
WireConnection;110;1;13;0
WireConnection;123;0;119;0
WireConnection;123;1;116;0
ASEEND*/
//CHKSM=C5F7FB486E90A5D40F020ABB3934510934071C59