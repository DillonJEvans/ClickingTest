shader_type spatial;

instance uniform float time_elapsed : hint_range(0, 1, 0.01) = 0;

group_uniforms Animation;
uniform float duration : hint_range(0, 1, 0.01) = 1;
uniform float fade_radius : hint_range(0, 0.5, 0.01) = 0.4;

group_uniforms Waves;
uniform int waves : hint_range(1, 5) = 3;
uniform float width : hint_range(0, 0.5, 0.01) = 0.1;
uniform float gap : hint_range(0, 0.5, 0.01) = 0.1;

group_uniforms Color;
uniform vec3 albedo : source_color = vec3(0, 0.4, 0.8);
uniform float alpha : hint_range(0, 1, 0.01) = 1;


void fragment() {
	float total_width = float(waves) * (width + gap) - gap;
	float speed = (0.5 + total_width) / duration;
	
	float minX = speed * time_elapsed - total_width;
	float maxX = speed * time_elapsed;
	
	float x = length(UV - vec2(0.5));
	
	float wave = (x - minX) / (width + gap);
	float wave_percent = width / (width + gap);
	
	if (wave < 0.0 || wave > float(waves) || fract(wave) > wave_percent) {
		ALPHA = 0.0;
	} else {
		ALBEDO = albedo;
		ALPHA = alpha;
		if (x > fade_radius) {
			float t = (0.5 - x) / (0.5 - fade_radius);
			// ALPHA *= t;
			ALPHA *= smoothstep(0, 1, t);
		}
	}
}
