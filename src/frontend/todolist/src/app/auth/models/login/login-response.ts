export interface LoginResponse {
    name: string,
    tokens: {
        accessToken: string,
        refreshToken: string
    }
}