shader_type canvas_item;

uniform sampler2D noise;
uniform float noise_intensity : hint_range(0, 0.5, 0.01) = 0;
uniform float noise_speed : hint_range(0, 0.1, 0.005) = 0;

uniform float radius : hint_range(0, 0.75, 0.01) = 0.5;
uniform float blur_thickness : hint_range(0, 0.75, 0.01) = 0.1;
uniform float max_alpha : hint_range(0, 1, 0.01) = 1;

const vec2 center = vec2(0.5);

float length_sq(vec2 v) {
	return v.x * v.x + v.y * v.y;
}

void fragment() {
	vec2 uv = UV - center;
	
	float theta = (atan(uv.y, uv.x) + PI) / TAU;
	vec2 noise_uv = fract(vec2(theta, noise_speed * TIME));
	float radius_offset = texture(noise, noise_uv).r * noise_intensity;
	
	float radius_sq = (radius + radius_offset) * (radius + radius_offset);
	float distance_sq = length_sq(uv);
	float blur_amount = (distance_sq - radius_sq) / blur_thickness;
	
	COLOR.a = smoothstep(0, 1, blur_amount) * max_alpha;
}
