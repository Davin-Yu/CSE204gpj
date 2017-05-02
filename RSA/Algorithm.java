package rsa;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.math.*;
import java.util.Random;

/**
 * RSA Algorithm
 * 
 * @author Davin_Yu
 */
public class Algorithm {
	
	private static final int maxBitLength = 200;
	private static final int certainty = 1000;
	private static byte[] bytePlainText;
	private static byte[] byteCipherText;
	private static String decryptedText;
	private static BigInteger M;
	private static BigInteger C;
	private static BigInteger backC;
	private static BigInteger p;
	private static BigInteger q;
	private static BigInteger n;
	private static BigInteger fei;
	private static BigInteger e;
	private static BigInteger d;
	
	private static void print() {
		System.out.println("M: " + M);
		System.out.println("p: " + p + " q: " + q);
		System.out.println("n: " + n + " fei: " + fei);
		System.out.println("e: " + e + " d: " + d);
		System.out.println("C: " + C);
		System.out.println("back C: " + backC);
		System.out.println("Decrypted Text: " + decryptedText);
	}
	
	private static BigInteger getRandomBigInteger(BigInteger upperBound) {
		BigInteger result;
		do {
			result = new BigInteger(upperBound.bitLength(), new Random());
	 	} while (result.compareTo(upperBound) >= 0);
		return result;
	}
	
	private static BigInteger getPrime() {
		return new BigInteger(maxBitLength, certainty, new Random());
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
	
	public static void main(String args[]) throws IOException {
		System.out.print("Please input a sentence (less than " + maxBitLength*2/8 + " char): ");
		BufferedReader in = new BufferedReader(new InputStreamReader(System.in));
		bytePlainText = in.readLine().getBytes();
		M = new BigInteger(bytePlainText);
		p = getPrime();
		q = getPrime();
		n = p.multiply(q);
		fei = p.add(new BigInteger("-1")).multiply(q.add(new BigInteger("-1")));
		e = getE(fei);
		d = e.modInverse(fei);
		C = encryption(M, e, n);
		backC = decryption(C, d, n);
		byteCipherText = backC.toByteArray();
		decryptedText = new String(byteCipherText, "US-ASCII");
		print();
	}

}
