'use server';

import { ActionResponse } from '@/types/actions';
import { getBaseApiUrl, getZodErrorsString } from '@/utils';
import { logError } from '@/utils/logError';
import { badRequest, ok, unauthorized } from '@/utils/server/actionResponses';
import { z } from 'zod';
import { loginUser } from '../loginUser';

const PASSWORD_REGEX =
  /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{8,}$/g;
const REGISTER_USER_SCHEMA = z.object({
  email: z.string().email(),
  password: z.string().regex(PASSWORD_REGEX, {
    message:
      'Password must contain at least one lowercase letter, one uppercase letter, one number, one special character, and be at least 8 characters long',
  }),
});
const REGISTER_ERROR_RESPONSE_SCHEMA = z.object({
  type: z.string(),
  title: z.string(),
  status: z.number(),
  errors: z.record(z.array(z.string())),
});

export type RegisterUserSchema = z.infer<typeof REGISTER_USER_SCHEMA>;

const handleRegisterFail = (data: unknown): ActionResponse<never> => {
  const parsedErrorData = REGISTER_ERROR_RESPONSE_SCHEMA.safeParse(data);

  if (!parsedErrorData.success)
    return badRequest(getZodErrorsString(parsedErrorData.error));

  const { errors } = parsedErrorData.data;
  const errorMessages = Object.entries(errors).map(
    ([field, messages]) => `${field}: ${messages.join(', ')}`,
  );

  return badRequest(errorMessages.join(', '));
};

const handleRegisterSuccess = async ({
  email,
  password,
}: RegisterUserSchema): Promise<ActionResponse<string>> => {
  await loginUser({ email, password });

  return ok(`User ${email} registered successfully`);
};

export const registerUser = async (
  data: RegisterUserSchema,
): Promise<ActionResponse<string | null>> => {
  if (process.env.ENABLE_USER_REGISTRATION !== '1')
    return badRequest('User registration is disabled');

  const parsedData = REGISTER_USER_SCHEMA.safeParse(data);

  if (!parsedData.success)
    return badRequest(getZodErrorsString(parsedData.error));

  const endpoint = `${getBaseApiUrl()}/register`;

  return await fetch(endpoint, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(parsedData.data),
  })
    .then(async (res) => {
      if (res.status === 401) return unauthorized();
      if (res.ok) return await handleRegisterSuccess(parsedData.data);

      const errorData = await res.json();

      return handleRegisterFail(errorData);
    })
    .catch((error) => {
      logError({
        error,
        filePath: 'src/app/actions/users/registerUser/registerUser.ts',
      });

      return badRequest('An error occurred');
    });
};
