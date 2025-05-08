// src/components/Navbar.js
import React, { useContext } from "react"
import { Link } from "react-router-dom"
import "../styles/Navbar.css"
import { AuthContext } from "../context/AuthContext"

// Site navigation bar
function Navbar() {
    const { isLoggedIn, logout } = useContext(AuthContext);

    return (
        <nav class="navbar">
            <ul>
                <li><Link to="/">Home</Link></li>
                <li><Link to="/movies">Movies</Link></li>
                <li><Link to="/recentreleases">Recent Releases</Link></li>

                {!isLoggedIn ? (
                    <>
                        <li><Link to="/register">Register</Link></li>
                        <li><Link to="/login">Login</Link></li>
                    </>
                ) : (
                    <>
                        <li><Link to="/my-movies">My Movies</Link></li>
                        <li><Link to="/user-dashboard">My Stats</Link></li>
                        <li><Link to="/addmovie">Add Movie</Link></li>
                        <button onClick={logout}>Logout</button>
                    </>
                )}
            </ul>
        </nav>
    );
}

export default Navbar;