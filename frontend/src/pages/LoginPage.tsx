"use client";

import React from "react";
import { useRouter } from "next/navigation";
import { isErrorResponse } from "@/shared/http/types";
import { useAuthStore } from "@/shared/auth/store";
import { LogIn } from "lucide-react";

export default function LoginPage() {
  const [login, setLogin] = React.useState("");
  const [password, setPassword] = React.useState("");

  const [error, setError] = React.useState<string | null>(null);
  const [loading, setLoading] = React.useState(false);

  const loadSelf = useAuthStore((s) => s.loadSelf);

  const router = useRouter();

  async function onLogin(e: React.SubmitEvent) {
    e.preventDefault();

    setError(null);
    setLoading(true);

    const fallback = "Login failed";

    try {
      const result = await fetch("/bff/identity/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ login, password }),
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
    <div className="flex min-h-[calc(100vh-57px)] flex-col items-center justify-center">
      <div className="w-72">
        <form onSubmit={onLogin} className="grid gap-3">
          <input
            type="text"
            name="login"
            placeholder="Login"
            value={login}
            onChange={(e) => setLogin(e.target.value)}
            className="rounded border border-[#1d2226] p-2"
          />
          <input
            type="password"
            name="password"
            placeholder="Password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            className="rounded border border-[#1d2226] p-2"
          />
          <div hidden={!error} className="text-center text-sm text-red-700">
            {error}
          </div>
          <button
            type="submit"
            className="cursor-pointer pt-3"
            disabled={loading || !login || !password}
          >
            {!loading ? (
              <div className="inline-flex items-center gap-2">
                <LogIn className="h-5 w-5" />
                <span>Login</span>
              </div>
            ) : (
              "..."
            )}
          </button>
        </form>
      </div>
    </div>
  );
}
