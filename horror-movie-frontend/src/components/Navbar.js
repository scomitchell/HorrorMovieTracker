import React from "react";
import { Link } from "react-router-dom";
import "../styles/Navbar.css"

function Navbar() {
    const isLoggedIn = !!localStorage.getItem("token");

    return (
        <nav>
            <ul>
                <li><Link to="/">Home</Link></li>
                <li><Link to="/movies">Movies</Link></li>
                <li><Link to="/recentreleases">Recent Releases</Link></li>
                <li><Link to="/my-movies">My Movies</Link></li>

                {!isLoggedIn && (
                    <>
                        <li><Link to="/register">Register</Link></li>
                        <li><Link to="/login">Login</Link></li>
                    </>
                )}
            </ul>
        </nav>
    );
}

export default Navbar;