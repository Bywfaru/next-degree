'use client';

import { registerUser } from '@/app/actions/users';
import { Button, Input } from '@/components';
import { zodResolver } from '@hookform/resolvers/zod';
import clsx from 'clsx';
import Link from 'next/link';
import { useRouter } from 'next/navigation';
import { type FC, useState } from 'react';
import { type SubmitHandler, useForm } from 'react-hook-form';
import { z } from 'zod';

const PASSWORD_REGEX =
  /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{8,}$/g;
const REGISTER_SCHEMA = z
  .object({
    email: z.string().email(),
    password: z.string().regex(PASSWORD_REGEX, {
      message:
        'Password must contain at least one lowercase letter, one uppercase letter, one number, one special character, and be at least 8 characters long',
    }),
    confirmPassword: z.string(),
  })
  .superRefine(({ password, confirmPassword }, ctx) => {
    if (password !== confirmPassword)
      ctx.addIssue({
        code: 'custom',
        message: 'Passwords do not match',
        path: ['confirmPassword'],
      });
  });

type RegisterSchema = z.infer<typeof REGISTER_SCHEMA>;

export const RegisterForm: FC = () => {
  const {
    handleSubmit,
    register,
    formState: { errors },
  } = useForm<RegisterSchema>({
    resolver: zodResolver(REGISTER_SCHEMA),
  });
  const router = useRouter();
  const [isLoading, setIsLoading] = useState(false);
  const [formError, setFormError] = useState<string | null>(null);

  const processForm: SubmitHandler<RegisterSchema> = async ({
    email,
    password,
  }) => {
    setIsLoading(true);

    const response = await registerUser({ email, password });

    if (response.success) return router.push('/dashboard/degrees');

    setFormError(response.error);
    setIsLoading(false);
  };

  return (
    <form
      onSubmit={handleSubmit(processForm)}
      className={clsx(['flex', 'flex-col', 'w-full', 'gap-2.5'])}
    >
      <Input
        type="email"
        register={register('email')}
        label="Email"
        placeholder="Enter your email address"
        errors={errors}
        disabled={isLoading}
        fullWidth
      />

      <Input
        type="password"
        register={register('password')}
        label="Password"
        placeholder="Enter your password"
        errors={errors}
        disabled={isLoading}
        fullWidth
      />

      <Input
        type="password"
        register={register('confirmPassword')}
        label="Confirm Password"
        placeholder="Re-enter your password"
        errors={errors}
        disabled={isLoading}
        fullWidth
      />

      {!!formError && (
        <p className={clsx('text-red-500', 'text-center')}>{formError}</p>
      )}

      <Button type="submit" loading={isLoading}>
        Register
      </Button>

      <p className="text-center">
        Have an account already?{' '}
        <Link
          href="/login"
          className={clsx(
            'underline',
            'text-primary',
            'hover:text-primary/50',
            'transition',
          )}
        >
          Login here
        </Link>
      </p>
    </form>
  );
};
