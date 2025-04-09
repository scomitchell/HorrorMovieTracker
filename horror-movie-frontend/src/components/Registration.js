import React, { useState, useContext } from "react";
import { useNavigate } from "react-router-dom"
import { AuthContext } from "../context/AuthContext";

const Register = () => {
	const [username, setUsername] = useState("");
	const [password, setPassword] = useState("");
	const [error, setError] = useState(null);
	const navigate = useNavigate();
	const { login, isLoggedIn } = useContext(AuthContext);

	const handleRegister = async (e) => {
		e.preventDefault();
		try {
			const response = await fetch("http://localhost:5004/api/auth/register", {
				method: "POST",
				headers: {
					"Content-Type": "application/json",
				},
				body: JSON.stringify({ username, password }),
			});

			if (!response.ok) {
				throw new Error("Registration failed");
			}

			const data = await response.json();
			login(data.token);
			navigate("/my-movies");
		} catch (error) {
			setError(error.message);
		}
	};

	return (
		<div>
			<h2>Register</h2>
			<form onSubmit={handleRegister}>
				<input
					type="text"
					placeholder="Username"
					value={username}
					onChange={(e) => setUsername(e.target.value)}
					required
				/>
				<input
					type="text"
					placeholder="Password"
					value={password}
					onChange={(e) => setPassword(e.target.value)}
				/>
				<button type="submit">Register</button>
			</form>
			{error && <p>{error}</p>}
		</div>
	);
};

export default Register;
