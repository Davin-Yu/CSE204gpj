package rsa;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.math.*;
import java.util.Random;

public class Algorithm {
	
	private static final BigInteger maxInt = new BigInteger("10000000000000"); 
	private static BigInteger plaintext;
	private static BigInteger ciphertext;
	private static BigInteger backtext;
	private static BigInteger p;
	private static BigInteger q;
	private static BigInteger n;
	private static BigInteger fei;
	private static BigInteger e;
	private static BigInteger d;
	
	private static void print() {
		System.out.println("plaintext: " + plaintext);
		System.out.println("p: " + p + " q: " + q);
		System.out.println("n: " + n + " fei: " + fei);
		System.out.println("e: " + e + " d: " + d);
		System.out.println("ciphertext: " + ciphertext);
		System.out.println("Tranfer back: " + backtext);
	}
	
	private static BigInteger getRandomBigInteger(BigInteger upperBound) {
	    Random rand = new Random();
	    BigInteger result;
	    do {
	    	result = new BigInteger(upperBound.bitLength(), rand);
	    } while (result.compareTo(upperBound) >= 0);
	    return result;
	}
	
	private static BigInteger getPrime() {
		BigInteger prime;
		do {
			prime = getRandomBigInteger(maxInt);
		} while ( !prime.isProbablePrime(1000) );
		return prime;
	}
	
	private static BigInteger getE(BigInteger fei) {
		BigInteger e;
		do {
			e = getRandomBigInteger(fei);
		} while (! (e.gcd(fei).compareTo(new BigInteger("1")) == 0) );
		return e;
	}
	
	private static BigInteger encryption(BigInteger M, BigInteger e, BigInteger n) {
		return M.modPow(e, n);
	}
	
	private static BigInteger decryption(BigInteger C, BigInteger d, BigInteger n) {
		return C.modPow(d, n);
	}
	
	public static void main(String args[]) {
		System.out.print("Please input a number (smaller than " + maxInt + "): ");
		BufferedReader in = new BufferedReader(new InputStreamReader(System.in));
		try {
			plaintext = new BigInteger(in.readLine());
		} catch (IOException e1) {
			e1.printStackTrace();
		}
		p = getPrime();
		q = getPrime();
		n = p.multiply(q);
		fei = p.add(new BigInteger("-1")).multiply(q.add(new BigInteger("-1")));
		e = getE(fei);
		d = e.modInverse(fei);
		ciphertext = encryption(plaintext, e, n);
		backtext = decryption(ciphertext, d, n);
		print();
	}

}
