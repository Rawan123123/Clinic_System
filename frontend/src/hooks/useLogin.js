import { API_BASE_URL } from '../config';
import { useState } from 'react'

export function useLogin() {
    const [loginErrors, setLoginErrors] = useState('')
    const [loginSuccess, setLoginSuccess] = useState('')

    async function login(loginData) {
        setLoginErrors('')
        setLoginSuccess('')

        try {
            const response = await fetch(
                `${API_BASE_URL}/api/Auth/Login`,
                {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(loginData)

                }
            )
            const data = await response.json()

            if (response.ok) {
                setLoginSuccess('Login is done');
                localStorage.setItem("MyToken", data.token);
                return true;

            } else {
                setLoginErrors(data.message || "invalid email or password")
                return false;
            }
        } catch (error) {
            setLoginErrors("Something went wrong. Please try again.");
            return false;
        }
    }
    return { loginErrors, loginSuccess, login }
}