import React, { useState, useContext } from "react"
import { useNavigate } from "react-router-dom"
import { AuthContext } from "../context/AuthContext"
import { Form, Button } from "react-bootstrap"
import "../styles/Login.css"

// Form that allows user to sign in to platform
const Login = () => {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState(null);
    const navigate = useNavigate();
    const { login, isLoggedIn } = useContext(AuthContext);

    const handleLogin = async (e) => {
        e.preventDefault();
        try {
            const response = await fetch("http://localhost:5004/api/auth/login", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({ username, password }),
            });

            if (!response.ok) {
                throw new Error("Invalid credentials");
            }

            const data = await response.json();
            login(data.token);
            navigate("/my-movies");
        } catch (error) {
            setError(error.message);
        }
    };

    if (isLoggedIn) {
        return <h1>You are already logged in</h1>;
    }

    return (
        <div class="login">
            <h2 className="mb-3">Login</h2>
            <Form onSubmit={handleLogin}>
                <Form.Group id="hmt-sign-in">
                    <Form.Control
                        className="mb-3"
                        type="text"
                        placeholder="username"
                        value={username}
                        onChange={(e) => setUsername(e.target.value)}
                        required />

                    <Form.Control
                        type="password"
                        placeholder="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        required />
                </Form.Group>
                <Button variant="primary" type="submit">Login</Button>
            </Form>
            {error && <p>{error}</p>}
        </div>
    );
};

export default Login;