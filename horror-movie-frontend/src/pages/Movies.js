import React, { useEffect, useState } from "react";
import "../styles/Search.css";

const MoviesPage = () => {
    const [allMovies, setAllMovies] = useState([]);
    const [searchTerm, setSearchTerm] = useState("");
    const [selectedSubgenre, setSelectedSubgenre] = useState("");
    const [message, setMessage] = useState("");

    useEffect(() => {
        const fetchAllMovies = async () => {
            try {
                const response = await fetch("http://localhost:5004/api/movies");
                if (!response.ok) {
                    throw new Error("Failed to fetch movies");
                }

                const data = await response.json();
                setAllMovies(data);
            } catch (error) {
                console.error(error);
            }
        };

        fetchAllMovies();
    }, []);

    const handleAddToMyList = async (movie) => {
        try {
            const response = await fetch("http://localhost:5004/api/usermovies", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    Authorization: `Bearer ${localStorage.getItem("token")}`,
                },
                body: JSON.stringify(movie),
            });

            const result = await response.text();

            if (!response.ok) {
                throw new Error(result);
            }

            setMessage(result);
        } catch (error) {
            setMessage(error.message);
        }
    };

    const uniqueSubgenres = [...new Set(allMovies.map(movie => movie.subgenre).filter(Boolean))];

    const filteredMovies = allMovies.filter((movie) =>
        movie.title.toLowerCase().includes(searchTerm.toLowerCase())
    && (selectedSubgenre === "" || movie.subgenre === selectedSubgenre));

    return (
        <div class="movies-container">
            <h1>Search Movies</h1>
            <input
                type="text"
                placeholder="Search by title"
                value={searchTerm}
                onChange={(e) => setSearchTerm(e.target.value)}
            />

            <select
                value={selectedSubgenre}
                onChange={(e) => setSelectedSubgenre(e.target.value)}
                style={{ marginLeft: "10px" }}
            >
                <option value="">All Subgenres</option>
                {uniqueSubgenres.map((sub, index) => (
                    <option key={index} value={sub}>
                        {sub}
                    </option>
                ))}
            </select>

            {message && <p>{message}</p>}

            <ul>
                {filteredMovies.map((movie) => (
                    <li key={movie.id}>
                        <h3>
                            {movie.title} ({new Date(movie.releaseDate).toLocaleDateString()})
                            <button onClick={() => handleAddToMyList(movie)} style={{ marginLeft: '10px' }}>
                                Add to My List
                            </button>
                        </h3>
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