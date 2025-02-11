declare global {
  namespace NodeJS {
    interface ProcessEnv {
      DEV_API_URL: string;
      PROD_API_URL: string;
    }
  }
}

export {};