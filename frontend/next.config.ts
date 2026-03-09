import type { NextConfig } from "next";
import { env } from "@/shared/env";

const gateway = process.env.BACKEND_GATEWAY_URL;
if (!gateway) {
  throw new Error("Missing env: BACKEND_GATEWAY_URL");
}

const nextConfig: NextConfig = {
  reactCompiler: true,
  async rewrites() {
    return [{ source: "/bff/:path*", destination: `${gateway}/api/:path*` }];
  },
};

export default nextConfig;
