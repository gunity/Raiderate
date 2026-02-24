function getEnv(name: string): string {
    const result = process.env[name];
    if (!result) {
        throw new Error(`Could not find env: ${name}`);
    }
    return result;
}

export const env = {
    gatewayInternalUrl: getEnv('gatewayInternalUrl'),
}