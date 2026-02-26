import type {NextConfig} from "next";
import {env} from "@/shared/env";

const nextConfig: NextConfig = {
    reactCompiler: true,
    async rewrites() {
        return [
            {source: "/bff/:path*", destination: `${env.gatewayInternalUrl}/api/:path*`},
        ];
    }
};

export default nextConfig;
