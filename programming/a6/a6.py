# solution by Gavin Pease

def is_prime(x):
    if x == 2:
        return True
    if x % 2 == 0:
        return False
    for i in range(3, int(x**0.5) + 1, 2):
        if x % i == 0:
            return False
    return True


def calculate_prime_factors(x):
    """
    This calculates the prime factors of a number.
    For example, the prime factors of 12 are 2, 2, and 3.
    :param x: The number to calculate the prime factors of
    """
    prime_factors = []
    i = 2
    while i * i <= x:
        if x % i:
            i += 1
        else:
            x //= i
            prime_factors.append(i)
    if x > 1:
        prime_factors.append(x)
    return prime_factors


def algo(x, counter=1):
    """
    This is the main algorith, it follows the following steps:
    1. Increment the counter.
    2. Check if the number is prime, if so, print it and return the counter.
    3. Calculate the prime factors of the number.
    4. Sum the prime factors and call the algorithm again with the sum as the
    new number.
    :param x: The number to check
    :param counter: The number of times the algorithm has been called
    """
    return (
        f"{x} {counter}"
        if is_prime(x)
        else algo(sum(calculate_prime_factors(x)), counter + 1)
    )


if __name__ == "__main__":
    # This reads the inputs from the user and calls the algorithm after each
    # input. The loop breaks when the user enters 4.
    while 1:
        x = int(input())
        if x == 4:
            break
        print(algo(x))
