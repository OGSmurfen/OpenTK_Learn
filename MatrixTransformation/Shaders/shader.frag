#version 330 core

in vec2 texPos;

out vec4 FragColor;

uniform sampler2D tex0;
uniform sampler2D tex1;

void main()
{
	FragColor = mix(texture(tex0, texPos), texture(tex1, texPos), 0.2);
}