## Random BigInteger Generator and Prime Test

This [**C#**](https://en.wikipedia.org/wiki/C_Sharp_(programming_language)) project shows Random **BigInteger** Generator by using the **Bit Length** of the integers and it's also shows the **Primality** test of the Big Random Integer.

In this project I used **[Miller-Rabin](https://en.wikipedia.org/wiki/Miller%E2%80%93Rabin_primality_test)** primality test as an extension method on the BigInteger type.

## File Description

[BigIntRandomGen.cs](https://github.com/arupmondal-cs/BigInteger-Random-Number-Generator-and-Prime-Test/blob/master/BigIntRandomGen.cs) contains the actual code for the Random BigInteger Generator and Prime Test.

  * **RandomBigIntegerGenerator** class contains two methods,
    * **NextBigInteger** takes input **_bit length_** to generate a Random BigInteger. 
    
    * **RandomBigInteger** function takes two inputs i.e., _start_ and _end_ (BigInteger type) integer limit to generate             randomly BigInteger within the specified range (In this program I include the fuction but I did not used this, if you         want you can use this function in your program).
    
  * **BigIntegerPrimeTest** class contains the **IsProbablePrime** function which takes two input one is _source_ (BigInteger     type, Integer that you want to test for **Primality**) and other one is the [_certainty_]                 (https://en.wikipedia.org/wiki/Miller%E2%80%93Rabin_primality_test) (int type, the number of rounds of testing to perform), and it returns **true** (number is prime) or **false** (number is not prime) value. 
  
  ## Debug and Run
  
  
