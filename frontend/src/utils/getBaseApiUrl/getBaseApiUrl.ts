export const getBaseApiUrl = () =>
  process.env.NODE_ENV === 'production'
    ? process.env.PROD_API_URL
    : process.env.DEV_API_URL;