"use client";

import Link from "next/link";
import {usePathname, useRouter} from "next/navigation";
import React, {useEffect, useState} from "react";
import {getIdentitySelf, identityLogout} from "@/shared/identity/api";
import {useAuthStore} from "@/shared/auth/store";

export default function Header() {

    const path = usePathname();

    const self = useAuthStore((s) => s.self);
    const loading = useAuthStore((s) => s.loading);
    const loadSelf = useAuthStore((s) => s.loadSelf);
    const logoutLocal = useAuthStore((s) => s.logout);

    const router = useRouter();

    useEffect(() => {
        (async () => {
            await loadSelf();
        })();
    }, [loadSelf]);

    const logout = async () => {
        await identityLogout();
        logoutLocal();
    };

    const headerActions = () => {
        if (loading) {
            return <>...</>;
        }

        if (!self) {
            return (<>
                <Link
                    href="/login"
                    hidden={path === "/login"}
                    className="border rounded p-2 cursor-pointer"
                >
                    Login
                </Link>
                <Link
                    href="/register"
                    hidden={path === "/register"}
                    className="border rounded p-2 cursor-pointer"
                >
                    Register
                </Link>
            </>);
        }

        return (
            <button
                className="border rounded p-2 cursor-pointer"
                onClick={logout}
            >
                Logout
            </button>
        );
    };

    return (
        <div
            className="border-b h-14 flex items-center justify-between px-2"
        >
            <Link href="/">
                Raiderate
            </Link>
            <div
                className="flex items-center gap-2"
            >
                {headerActions()}
            </div>
        </div>
    );
}