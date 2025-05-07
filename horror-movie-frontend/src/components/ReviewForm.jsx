import React, { useState } from "react"
import { useNavigate } from "react-router-dom"
import "../styles/Reviews.css"

// Form that collects user reviews on a specific movie
const ReviewForm = ({ movieId }) => {
    const [content, setContent] = useState("");
    const [rating, setRating] = useState(5);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    const handleAddReview = async (e) => {
        e.preventDefault();
        try {
            const token = localStorage.getItem("token");

            const response = await fetch("http://localhost:5004/api/reviews", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    Authorization: `Bearer ${token}`,
                },
                body: JSON.stringify({
                    content,
                    rating,
                    movieId
                }),
            });

            if (!response.ok) {
                throw new Error("Failed to submit review");
            }

            setContent("");
            setRating(5);
            window.location.reload();
        } catch (error) {
            setError(error.message);
        }
    };

    return (
        <div className="review-form">
            <h2>Leave a review</h2>
            <form onSubmit={handleAddReview}>
                <textarea
                    placeholder="Write your review..."
                    value={content}
                    onChange={(e) => setContent(e.target.value)}
                    required
                    rows={4}
                />
                <div className="rating-submit">
                    <label htmlFor="rating">Rating (1 to 5):</label>
                    <select
                        value={rating}
                        onChange={(e) => setRating(parseInt(e.target.value))}
                        required
                    >
                        {[1, 2, 3, 4, 5].map((n) => (
                            <option key={n} value={n}>
                                {n}
                            </option>
                        ))}
                    </select>
                    <button type="submit">Submit Review</button>
                </div>
                {error && <p>{error}</p>}
            </form>
        </div>
    );
};

export default ReviewForm;