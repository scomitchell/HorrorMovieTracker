import React, { useEffect, useState } from "react"
import { Link } from "react-router-dom"
import "../styles/MyMovies.css"

const MyMoviesPage = () => {
    const [movies, setMovies] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchMyMovies = async () => {
            try {
                const response = await fetch("http://localhost:5004/api/usermovies/my-movies", {
                    headers: {
                        Authorization: `Bearer ${localStorage.getItem("token")}`,
                    },
                });

                if (!response.ok) {
                    throw new Error("Unable to retrieve your movies");
                }

                const data = await response.json();
                setMovies(data);
            } catch (error) {
                setError(error.message);
            } finally {
                setLoading(false);
            }
        };

        fetchMyMovies();
    }, []);

    const handleRemoveFromMyList = async (movieId) => {
        try {
            const response = await fetch(`http://localhost:5004/api/usermovies/${movieId}`, {
                method: "DELETE",
                headers: {
                    "Content-Type": "application/json",
                    Authorization: `Bearer ${localStorage.getItem("token")}`,
                },
            });

            if (response.ok) {
                setMovies(movies.filter((movie) => movie.id !== movieId));
                console.log("Movie removed from your list");
            } else {
                console.log("Error removing movie from list");
            }
        } catch (error) {
            console.error("Error:", error);
        }
    };

    if (loading) {
        return <p>Loading...</p>;
    }

    if (error) {
        return <div>Error: {error}</div>;
    }

    if (movies.length === 0) {
        return <div>No movies found</div>
    }

    return (
        <div class="my-movies">
            <h1>Your Movies</h1>
            <ul>
                {movies.map((movie) => (
                    <li key={movie.id}>
                        <h3>
                            <Link to={`/movies/${movie.id}`}>
                                {movie.title} ({new Date(movie.releaseDate).toLocaleDateString()})
                            </Link>
                            <button
                                onClick={() => handleRemoveFromMyList(movie.id)}
                                className="remove-button"
                            >
                                Remove
                            </button>
                        </h3>
                        {movie.imageUrl && (
                            <Link to={`/movies/${movie.id}`}>
                                <img src={movie.imageUrl}
                                    alt={`${movie.title} Poster`}
                                    style={{
                                        width: "150px",
                                        borderRadius: "8px"
                                    }}
                                    className="movie-cover"
                                />
                            </Link>
                        )}
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default MyMoviesPage;