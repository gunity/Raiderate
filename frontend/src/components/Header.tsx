"use client";

import Link from "next/link";
import {usePathname} from "next/navigation";
import React, {useEffect, useState} from "react";
import {getIdentitySelf} from "@/shared/identity/api";
import {IdentitySelf} from "@/shared/identity/types";

export default function Header() {

    const path = usePathname();
    const [self, setSelf] = React.useState<IdentitySelf | null>(null);
    const [loading, setLoading] = React.useState(true);

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
            <Link
                href="/logout"
                className="border rounded p-2 cursor-pointer"
            >
                Logout
            </Link>
        );
    };

    useEffect(() => {
        (async () => {
            setSelf(await getIdentitySelf());
            setLoading(false);
        })();
    }, []);

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