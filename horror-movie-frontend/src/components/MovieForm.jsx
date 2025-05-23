import React, { useState } from "react"
import { useNavigate } from "react-router-dom"
import "../styles/MovieForm.css"

// Form that allows user to add new movie to database and personal list
const MovieForm = () => {
    const [title, setTitle] = useState("");
    const [subgenre, setSubgenre] = useState("");
    const [releaseDate, setReleaseDate] = useState("");
    const [imageUrl, setImageUrl] = useState("");
    const [description, setDescription] = useState("");
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    const handleAddMovie = async (e) => {
        e.preventDefault();
        try {
            const token = localStorage.getItem("token");

            const response = await fetch("http://localhost:5004/api/usermovies", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    Authorization:`Bearer ${token}`,
                },
                body: JSON.stringify({
                    title,
                    subgenre,
                    releaseDate,
                    imageUrl,
                    description,
                }),
            });

            if (!response.ok) {
                throw new Error("Failed to add movie");
            }

            navigate("/my-movies");
        } catch (error) {
            setError(error.message);
        }
    };

    return (
        <div class="movie-form">
            <h2>Add a movie to the database</h2>
            <form onSubmit={handleAddMovie}>
                <input
                    type="text"
                    placeholder="title"
                    value={title}
                    onChange={(e) => setTitle(e.target.value)}
                    required
                />
                <input
                    type="text"
                    placeholder="subgenre"
                    value={subgenre}
                    onChange={(e) => setSubgenre(e.target.value)}
                    required
                />
                <input
                    type="date"
                    value={releaseDate}
                    onChange={(e) => setReleaseDate(e.target.value)}
                    required
                />
                <input
                    type="text"
                    placeholder="Image URL"
                    value={imageUrl}
                    onChange={(e) => setImageUrl(e.target.value)}
                />
                <input
                    type="text"
                    placeholder="Description"
                    value={description}
                    onChange={(e) => setDescription(e.target.value)}
                />
                <button type="submit">Add Movie</button>
                {error && <p>{error}</p>}
            </form>
        </div>
    );
};

export default MovieForm;