"use client";

import Link from "next/link";
import {usePathname, useRouter} from "next/navigation";
import React, {useEffect, useState} from "react";
import {getIdentitySelf, identityLogout} from "@/shared/identity/api";
import {useAuthStore} from "@/shared/auth/store";
import {LogIn, LogOut, UserPlus} from "lucide-react";

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
            return <></>;
        }

        if (!self) {
            return (<>
                <Link
                    href="/login"
                    hidden={path === "/login"}
                    className="inline-flex gap-2 items-center p-2 cursor-pointer hover:text-[#ffbe00]"
                >
                    <LogIn className="w-5 h5"/>
                    Login
                </Link>
                <Link
                    href="/register"
                    hidden={path === "/register"}
                    className="inline-flex gap-2 items-center p-2 cursor-pointer hover:text-[#ffbe00]"
                >
                    <UserPlus className="w-5 h5"/>
                    Register
                </Link>
            </>);
        }

        return (
            <button
                className="inline-flex gap-2 items-center p-2 cursor-pointer hover:text-[#ffbe00]"
                onClick={logout}
            >
                <LogOut className="h-5 w-5"/>
                Logout
            </button>
        );
    };

    return (
        <div
            className="border-b border-[#1d2226] bg-black h-14 flex items-center justify-between px-6"
        >
            <Link href="/" className="text-[#eee4d0] font-bold">
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