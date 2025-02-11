export type LogErrorParams = {
  error: unknown;
  filePath: string;
}

export const logError = ({ error, filePath }: LogErrorParams) => {
  console.error(`[ERROR] ${filePath}:`, error);
};