import { useEffect, useState } from "react";
import { get } from "./lib/api";

export default function App() {
    const [data, setData] = useState([]);
    const [err, setErr] = useState("");
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        // Adjust this path if your controller route differs. Check Swagger.
        get("/api/dumpsters")
            .then(setData)
            .catch(e => setErr(e.message || String(e)))
            .finally(() => setLoading(false));
    }, []);

    if (loading) return <div style={{ padding: 16 }}>Loading…</div>;
    if (err) return <div style={{ padding: 16, color: "red" }}>Error: {err}</div>;

    return (
        <div style={{ padding: 16 }}>
            <h1>Dumpsters</h1>
            <ul>
                {data.map((d) => (
                    <li key={d.id}>
                        {d.sizeYards} yd³ — ${d.price} — {d.description}
                    </li>
                ))}
            </ul>
        </div>
    );
}
