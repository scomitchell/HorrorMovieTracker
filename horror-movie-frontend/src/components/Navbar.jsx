import React, { useContext } from "react"
import { Link } from "react-router-dom"
import { ListGroup, Button } from "react-bootstrap"
import "../styles/Navbar.css"
import { AuthContext } from "../context/AuthContext"

// Site navigation bar
function Navbar() {
    const { isLoggedIn, logout } = useContext(AuthContext);

    return (
        <nav className="navbar d-none d-md-block">
            <ListGroup className="navbar-items fs-2 list-group-horizontal">
                <ListGroup.Item className="list-group-item">
                    <Link to="/">Home</Link>
                </ListGroup.Item>
                <ListGroup.Item className="list-group-item">
                    <Link to="/movies">Movies</Link>
                </ListGroup.Item>
                <ListGroup.Item className="list-group-item">
                    <Link to="/recentreleases">Recent Releases</Link>
                </ListGroup.Item>

                {!isLoggedIn ? (
                    <>
                        <ListGroup.Item className="list-group-item">
                            <Link to="/register">Register</Link>
                        </ListGroup.Item>
                        <ListGroup.Item className="list-group-item">
                            <Link to="/login">Login</Link>
                        </ListGroup.Item>
                    </>
                ) : (
                    <>
                            <ListGroup.Item className="list-group-item">
                                <Link to="/my-movies">My Movies</Link>
                            </ListGroup.Item>
                            <ListGroup.Item className="list-group-item">
                                <Link to="/addmovie">Add Movie</Link>
                            </ListGroup.Item>
                            <ListGroup.Item className="list-group-item">
                                <Button
                                    className="logout-button bg-black p-0"
                                    onClick={logout}>Logout</Button>
                            </ListGroup.Item>
                    </>
                )}
            </ListGroup>
        </nav>
    );
}

export default Navbar;