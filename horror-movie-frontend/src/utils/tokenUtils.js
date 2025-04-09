// Decode a base64url string
function base64UrlDecode(str) {
    try {
        const base64 = str.replace(/-/g, "+").replace(/_/g, "/");
        const jsonPayload = decodeURIComponent(
            atob(base64)
                .split("")
                .map(c => "%" + ("00" + c.charCodeAt(0).toString(16)).slice(-2))
                .join("")
        );
        return JSON.parse(jsonPayload);
    } catch (error) {
        return null;
    }
}

export function decodeJWT(token) {
    const parts = token.split(".");
    if (parts.length !== 3) return null;
    return base64UrlDecode(parts[1]);
}

export function isTokenExpired(token) {
    const payload = decodeJWT(token);
    if (!payload || !payload.exp) return true;
    const currentTime = Math.floor(Date.now() / 1000);
    return payload.exp < currentTime;
}

export function isTokenValid(token) {
    return token && !isTokenExpired(token);
}

// helper that deletes expired tokens
export function checkAndClearExpiredToken() {
    const token = localStorage.getItem("token");
    if (!token || isTokenExpired(token)) {
        localStorage.removeItem("token");
        return false;
    }
    return true;
}

