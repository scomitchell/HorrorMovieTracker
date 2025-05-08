import React, { useEffect, useState } from 'react'
import "../styles/UserDashboard.css"

const UserDashboard = () => {
    const [summary, setSummary] = useState(null);
    const [error, setError] = useState(null);

    useEffect(() => {
        const token = localStorage.getItem("token");

        fetch("http://localhost:5004/api/analytics/summary", {
            headers: {
                Authorization: `Bearer ${token}`,
            },
        })
            .then((res) => {
                if (!res.ok) throw new Error("Failed to fetch analytics");
                return res.json();
            })
            .then(setSummary)
            .catch((err) => setError(err.message));
    }, []);

    if (error) return <p>Error: {error}</p>
    if (!summary) return <p>Loading dashboard...</p>

    return (
        <div class="user-dash">
            <h2>User Analytics Dashboard</h2>
            <ul>
                <li>Movies Watched: {summary.watchedCount}</li>
                <li>Reviews Written: {summary.reviewCount}</li>
                <li>Average Rating: {summary.averageRating}</li>
                <li>Favorite Subgenre: {summary.topSubgenre}</li>
            </ul>
        </div>
    );
};

export default UserDashboard;