import React, { useEffect, useState } from "react"
import { Link } from "react-router-dom"
import "../styles/Home.css"

// Home page
const Home = () => {
    const [movies, setMovies] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchMovies = async () => {
            try {
                const response = await fetch("http://localhost:5004/api/movies");

                if (!response.ok) {
                    throw new Error("Could not fetch movies");
                }

                const data = await response.json();

                // Pull 5 most recently added
                const fiveMostRecent = data
                    .sort((a, b) => b.id - a.id)
                    .slice(0, 5);

                setMovies(fiveMostRecent);
            } catch (error) {
                setError(error.message);
            } finally {
                setLoading(false);
            }
        };

        fetchMovies();
    }, [])

    if (loading) {
        return <p>Loading...</p>
    }

    if (error) {
        return <p>Error: {error}</p>
    }

    return (
        <div className="home">
            <h1 className="mb-2 mt-3">Welcome to the Horror Movie Tracker</h1>
            <h2 className="mb-3">Recently Added</h2>

            {/* Horizontally scrollable movie list */}
            <div className="overflow-auto">
                <ul>
                    {movies.map((movie) => (
                        <li key={movie.id}>
                            {movie.imageUrl && (
                                <Link to={`/movies/${movie.id}`}>
                                    <img
                                        src={movie.imageUrl}
                                        alt={`${movie.title} Poster`}
                                        style={{
                                            width: '200px',
                                            height: '300px',
                                            borderRadius: '8px',
                                            objectFit: 'cover',
                                        }}
                                        className="movie-cover"
                                    />
                                </Link>
                            )}
                        </li>
                    ))}
                </ul>
            </div>
        </div>
    );
}

export default Home;