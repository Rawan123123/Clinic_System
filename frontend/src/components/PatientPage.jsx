import { useState , useEffect } from 'react'
import { useNavigate } from "react-router-dom";

function PatientPage() {

    const [patients, setPatients] = useState([])
    const [loading, setLoading] = useState(true)
    const [error, setError] = useState('')

    function getGender(gender) {
        return gender === 0 ? "Male" : "Female";
    }
    const navigate = useNavigate();

    useEffect(() => {
        async function fetchPatients() {
            const token = localStorage.getItem("MyToken");

            const response = await fetch(
                "https://localhost:7279/api/Patient",
                {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }

                }
            );
            if (response.ok) {
                const data = await response.json();
                setPatients(data);

            } else {
                setError('Failed to load patients')
            }
            setLoading(false);
        }
        fetchPatients();
    }
        , [])

    if (loading) return <p>Loading...</p>
    if (error) return <p>{error}</p>

    function handleLogOut() {
        localStorage.removeItem("MyToken")
        navigate("/login");
    }


    return(
        <div>
            <h2>My Patients</h2>
            <ul>
                {patients.map(p => (
                    <li key={p.patientId}>
                        {`Name: ${p.name}
                          ,Age: ${p.age}
                          ,Gender: ${(getGender(p.gender))}`}
                    </li>
                ))}
            </ul>

            <button type="button" onClick={() => navigate("/")}>Home</button>
            <button type="button" onClick={() => {handleLogOut()}}>LogOut</button>

        </div>
    )
    

}
export default PatientPage;