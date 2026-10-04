import { useState, useEffect } from 'react';
import { API_BASE_URL } from '../config';
export function usePatients() {
    const [patients, setPatients] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState('');

    useEffect(() => {
        async function fetchPatients() {
            const token = localStorage.getItem("MyToken");
            try {
                const response = await fetch(
                    `${API_BASE_URL}/api/Patient`,
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
            }
            catch {
                setError('Cannot reach the server');
            }
            finally {
                setLoading(false);
            }
        }
        fetchPatients();
    }, []);

    return { patients, loading, error }
}