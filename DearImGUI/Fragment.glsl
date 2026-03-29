#version 330 core

in vec2 fragUV;
in vec4 fragCol;

uniform sampler2D tex;

out vec4 color;

void main() {
    color = texture(tex, fragUV) * fragCol;
}