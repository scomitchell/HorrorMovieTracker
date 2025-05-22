import React, { useEffect, useState } from "react"
import { Link } from "react-router-dom"
import { Form, Button } from "react-bootstrap"
import "../styles/Search.css"
import "../styles/Universal.css"

// Displays all movies, allows search by title and filter by subgenre
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
            {message && <p className="ms-3">{message}</p>}

            <h2 className="page-header">Search Movies</h2>
            <div className="d-flex">
            <   Form.Control
                    className="search-bar"
                    type="text"
                    placeholder="Search by title"
                    value={searchTerm}
                    onChange={(e) => setSearchTerm(e.target.value)}
                />

                <Form.Select
                    className="select-subgenre"
                    value={selectedSubgenre}
                    onChange={(e) => setSelectedSubgenre(e.target.value)}
                >
                    <option value="">All Subgenres</option>
                    {uniqueSubgenres.map((sub, index) => (
                        <option key={index} value={sub}>
                            {sub}
                        </option>
                    ))}
                </Form.Select>
            </div>
            <hr className="page-divider-nrml"/>
            <ul>
                {filteredMovies.map((movie) => (
                    <li key={movie.id} className="mb-3">
                        <h3>
                            <Link to={`/movies/${movie.id}`}>
                                {movie.title} ({new Date(movie.releaseDate).toLocaleDateString()})
                            </Link>
                            <Button className="add-movie-btn" onClick={() => handleAddToMyList(movie)} style={{ marginLeft: '10px' }}>
                                Add to My List
                            </Button>
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

export default MoviesPage;