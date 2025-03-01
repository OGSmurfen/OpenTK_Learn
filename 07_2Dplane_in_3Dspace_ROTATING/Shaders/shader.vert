#version 330 core

layout(location = 0) in vec3 aPos;
layout(location = 1) in vec2 texPos;

out vec2 texturePosition;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
	gl_Position = vec4(aPos, 1f) * model * view * projection;
	texturePosition = texPos;
}