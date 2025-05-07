import React, { useEffect, useState } from "react"
import { Link } from "react-router-dom"
import "../styles/RecentReleases.css"

// Displays all movies released within the past year
const RecentReleasesPage = () => {
    const [movies, setMovies] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchMovies = async () => {
            try {
                const response = await fetch("http://localhost:5004/api/movies/recent/");
                if (!response.ok) {
                    throw new Error("Failed to fetch movies");
                }
                const data = await response.json();
                setMovies(data);
            } catch (error) {
                setError(error.message);
            } finally {
                setLoading(false);
            }
        };

        fetchMovies();
    }, [])

    if (loading) return <p>Loading...</p>
    if (error) return <p>Error: {error}</p>

    return (
        <div class="recent-releases">
            <h2>Recent Releases (Past Year)</h2>
            {movies.length === 0 ? (
                <p>No movies released in the past month.</p>
            ) : (
                <ul>
                    {movies.map((movie) => (
                        <li key={movie.id}>
                            <Link to={`/movies/${movie.id}`}>
                                 <h3>{movie.title} ({new Date(movie.releaseDate).toLocaleDateString()})</h3>
                            </Link>
                            {movie.imageUrl && (
                                <Link to={`/movies/${movie.id}`}>
                                    <img src={movie.imageUrl}
                                        alt={`${movie.title} Poster`}
                                        style={{
                                            width: "150px",
                                            borderRadius: "8px",
                                            objectFit: "cover",
                                        }}
                                        className = "movie-cover"
                                    />
                                </Link>
                            )}
                        </li>
                    ))}
                </ul>
            )}
        </div>
    );
}


export default RecentReleasesPage;