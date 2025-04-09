import React, { useEffect, useState } from "react"
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
    }, [])

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
                        <h3>{movie.title} ({new Date(movie.releaseDate).toLocaleDateString()})</h3>
                        {movie.imageUrl && (
                            <img src={movie.imageUrl} alt={`${movie.title} Poster`} style={{ width: "150px", borderRadius: "8px" }} />
                        )}
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default MyMoviesPage;