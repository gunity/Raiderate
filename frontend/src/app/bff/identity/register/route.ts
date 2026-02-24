import {NextResponse} from "next/server";
import {env} from "@/shared/env";

export async function POST(request: Request) {
    const body = await request.text();

    const response = await fetch(`${env.gatewayInternalUrl}/api/identity/register`, {
        method: 'POST',
        headers: {"Content-Type": "application/json"},
        body,
    });

    const result = new NextResponse(await response.text(), {status: response.status});

    const cookies = response.headers.getSetCookie?.() ?? [];
    for (const cookie of cookies) {
        result.headers.append("set-cookie", cookie);
    }

    return result;
}