import React, { useEffect, useState } from "react"
import "../styles/Home.css"

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
        <div class="home">
            <h1>Welcome to the Horror Movie Tracker</h1>
            <ul>
                {movies.map((movie) => (
                    <li key={movie.id}>
                        {movie.imageUrl && (
                            <img src={movie.imageUrl} alt={`${movie.title} Poster`} style={{ width: "200px", borderRadius: "8px" }} />
                        )}
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default Home;