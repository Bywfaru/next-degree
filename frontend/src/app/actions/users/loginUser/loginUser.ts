'use server';

import { ActionResponse } from '@/types/actions';
import { getBaseApiUrl, getZodErrorsString } from '@/utils';
import { logError } from '@/utils/logError/logError';
import { badRequest } from '@/utils/server/actionResponses';
import { cookies } from 'next/headers';
import { redirect } from 'next/navigation';
import { z } from 'zod';

const LOGIN_USER_SCHEMA = z.object({
  email: z.string().email(),
  password: z.string(),
});
const LOGIN_ERROR_RESPONSE_SCHEMA = z.object({
  type: z.string(),
  title: z.string(),
  status: z.number(),
  detail: z.string(),
});
const LOGIN_SUCCESS_RESPONSE_SCHEMA = z.object({
  tokenType: z.string(),
  accessToken: z.string(),
  refreshToken: z.string(),
  expiresIn: z.number(),
});

export type LoginUserParams = z.infer<typeof LOGIN_USER_SCHEMA>;

const handleSuccessfulLogin = async (data: unknown) => {
  const parsedData = LOGIN_SUCCESS_RESPONSE_SCHEMA.safeParse(data);

  if (!parsedData.success)
    return badRequest(getZodErrorsString(parsedData.error));

  const cookieStore = await cookies();

  cookieStore.set('accessToken', parsedData.data.accessToken, {
    maxAge: parsedData.data.expiresIn,
  });

  return redirect('/degrees');
};

const handleFailedLogin = (data: unknown): ActionResponse<never> => {
  const parsedErrorData = LOGIN_ERROR_RESPONSE_SCHEMA.safeParse(data);

  if (!parsedErrorData.success)
    return badRequest(getZodErrorsString(parsedErrorData.error));

  return badRequest(
    'Login failed. Please check your email and password and try again.',
  );
};

export const loginUser = async (data: LoginUserParams) => {
  const parsedData = LOGIN_USER_SCHEMA.safeParse(data);

  if (!parsedData.success)
    return badRequest(getZodErrorsString(parsedData.error));

  const endpoint = `${getBaseApiUrl()}/login`;

  return await fetch(endpoint, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(parsedData.data),
  })
    .then(async (res) => {
      if (res.ok) return handleSuccessfulLogin(await res.json());

      const errorData = await res.json();

      return handleFailedLogin(errorData);
    })
    .catch((error) => {
      logError({
        error,
        filePath: 'src/app/actions/users/loginUser/loginUser.ts',
      });

      return badRequest('An error occurred');
    });
};
