export interface RegisterResponse {
    name: string,
    tokens: {
        accessToken: string,
        refreshToken: string
    }
}