import { useState } from 'react'
import { API_BASE_URL } from '../config';

export function useRegister() {
    const [registerErrors, setRegisterErrors] = useState([])
    const [registerSuccess, setRegisterSuccess] = useState('')

    async function register(registerData) {
        setRegisterErrors([])
        setRegisterSuccess('')

        try {
            const response = await fetch(
                `${API_BASE_URL}/api/Auth/Register`,
                {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(registerData)
                }
            )
            const data = await response.json()
            if (response.ok) {
                setRegisterSuccess('Registration is successful')
            } else {

                if (data.errors) {
                    const allErrors = Object.values(data.errors).flat()
                    setRegisterErrors(allErrors)
                }
                else {
                    setRegisterErrors([data.message || "try again"])
                }
            }
        } catch (error) {
            setRegisterErrors(["Something went wrong. Please try again."]);
        }
    }
    return { registerErrors, registerSuccess, register }
}