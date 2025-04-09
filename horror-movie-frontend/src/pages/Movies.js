import React, { useEffect, useState } from "react"

const MoviesPage = () => {
    const [movies, setMovies] = useState([]); // Initialize as an empty array
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchMovies = async () => {
            try {
                const response = await fetch("http://localhost:5004/api/movies");
                if (!response.ok) {
                    throw new Error("Failed to fetch movies");
                }
                const data = await response.json();
                setMovies(data); // Update the state with fetched data
            } catch (error) {
                setError(error.message); // If there's an error, set the error state
            } finally {
                setLoading(false); // Set loading to false after the request completes
            }
        };

        fetchMovies();
    }, []);

    if (loading) {
        return <div>Loading...</div>;
    }

    if (error) {
        return <div>Error: {error}</div>;
    }

    if (movies.length === 0) {
        return <div>No movies found.</div>;
    }

    return (
        <div>
            <h1>Movies</h1>
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

export default MoviesPage;