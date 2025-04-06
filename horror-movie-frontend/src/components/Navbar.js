// src/components/Navbar.js
import React, { useContext } from "react";
import { Link } from "react-router-dom";
import "../styles/Navbar.css";
import { AuthContext } from "../context/AuthContext";

function Navbar() {
    const { isLoggedIn, logout } = useContext(AuthContext);

    return (
        <nav>
            <ul>
                <li><Link to="/">Home</Link></li>
                <li><Link to="/movies">Movies</Link></li>
                <li><Link to="/recentreleases">Recent Releases</Link></li>
                <li><Link to="/addmovie">Add Movie</Link></li>
                <li><Link to="/searchmovies">Search</Link></li>
                <li><Link to="/my-movies">My Movies</Link></li>

                {!isLoggedIn ? (
                    <>
                        <li><Link to="/register">Register</Link></li>
                        <li><Link to="/login">Login</Link></li>
                    </>
                ) : (
                    <li>
                        <button onClick={logout}>Logout</button>
                    </li>
                )}
            </ul>
        </nav>
    );
}

export default Navbar;