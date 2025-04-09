import React, { useEffect, useState } from "react";
import "../styles/RecentReleases.css"

const RecentReleasesPage = () => {
    const [movies, setMovies] = useState([]);
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

                // Get one month ago
                const oneMonthAgo = new Date();
                oneMonthAgo.setMonth(oneMonthAgo.getMonth() - 1);

                // Filter movies
                const recentMovies = data.filter(movie => {
                    const releaseDate = new Date(movie.releaseDate);
                    return releaseDate >= oneMonthAgo;
                });

                setMovies(recentMovies);
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
        <div>
            <h2>Recent Releases (Past Month)</h2>
            {movies.length === 0 ? (
                <p>No movies released in the past month.</p>
            ) : (
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
            )}
        </div>
    );
}


export default RecentReleasesPage;