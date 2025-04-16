import React, { createContext, useState, useEffect } from "react"
import { isTokenValid } from "../utils/tokenUtils"

export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [isLoggedIn, setIsLoggedIn] = useState(() => {
        const token = localStorage.getItem("token");
        return token && isTokenValid(token);
    });

    useEffect(() => {
        const token = localStorage.getItem("token");
        if (!token || !isTokenValid(token)) {
            localStorage.removeItem("token");
            setIsLoggedIn(false);
        }
    }, []);

    const login = (token) => {
        localStorage.setItem("token", token);
        setIsLoggedIn(true);
    };

    const logout = () => {
        localStorage.removeItem("token");
        setIsLoggedIn(false);
    };

    return (
        <AuthContext.Provider value={{ isLoggedIn, login, logout }}>
            {children}
        </AuthContext.Provider>
    );
};