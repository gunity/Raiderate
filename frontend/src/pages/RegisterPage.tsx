"use client";

import React from "react";
import {useRouter} from "next/navigation";
import {isErrorResponse} from "@/shared/http/types";
import {useAuthStore} from "@/shared/auth/store";

export default function RegisterPage() {

    const [login, setLogin] = React.useState("");
    const [password, setPassword] = React.useState("");
    const [passwordConfirm, setPasswordConfirm] = React.useState("");

    const [error, setError] = React.useState<string | null>(null);
    const [loading, setLoading] = React.useState(false);

    const loadSelf = useAuthStore((s) => s.loadSelf);

    const router = useRouter();

    async function onRegister(e: React.SubmitEvent) {
        e.preventDefault();

        setError(null);
        setLoading(true);

        const fallback = "Register failed";

        if (password != passwordConfirm) {
            setError("Passwords do not match");
            setLoading(false);
            return;
        }

        try {
            const result = await fetch("/bff/identity/register", {
                method: "POST",
                headers: {"Content-Type": "application/json"},
                body: JSON.stringify({login, password}),
            });

            if (!result.ok) {
                const json: unknown = await result.json().catch(() => null);

                if (isErrorResponse(json)) {
                    throw new Error(json.message);
                } else {
                    throw new Error(fallback);
                }
            }

            await loadSelf();
            router.replace("/");
        } catch (e: unknown) {
            setError(e instanceof Error ? e.message : fallback);
        } finally {
            setLoading(false);
        }
    }

    return (
        <div className="min-h-screen flex flex-col items-center justify-center">
            <div className="w-72">
                <form
                    onSubmit={onRegister}
                    className="grid gap-3">
                    <input
                        type="text"
                        name="login"
                        placeholder="Login"
                        value={login}
                        onChange={e => setLogin(e.target.value)}
                        className="border rounded p-2"
                    />
                    <input
                        type="password"
                        name="password"
                        placeholder="Password"
                        value={password}
                        onChange={e => setPassword(e.target.value)}
                        className="border rounded p-2"
                    />
                    <input
                        type="password"
                        name="passwordConfirm"
                        placeholder="Re-type password"
                        value={passwordConfirm}
                        onChange={e => setPasswordConfirm(e.target.value)}
                        className="border rounded p-2"
                    />
                    <div
                        hidden={!error}
                        className="text-sm text-red-700 text-center"
                    >
                        {error}
                    </div>
                    <button
                        type="submit"
                        className="border rounded p-2 cursor-pointer"
                        disabled={loading || !login || !password}
                    >
                        {!loading ? "Register" : "Loading"}
                    </button>
                </form>
            </div>
        </div>
    );
}