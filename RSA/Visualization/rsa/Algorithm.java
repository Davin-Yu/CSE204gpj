package rsa;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.UnsupportedEncodingException;
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
	private static String stringPlainText;
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
	
	public static String[] print() {
		String s[] = new String[7];
		s[0] = "Plain Text (String): " + stringPlainText + "\r\n" + 
			   "Plain Text (Byte): " + M + "\r\n";
		s[1] = "First, we randomly Pick two Prime 'p' and 'q'" + "\r\n" +
			   "p: " + p + "\r\n" +
			   "q: " + q + "\r\n";
		s[2] = "n=pq, n: " + n + "\r\n" +
			   "f=(p-1)(q-1), f: " + fei + "\r\n";
		s[3] = "We found 'e' which is relative inverse to f" + "\r\n" +
			   "e: " + e + "\r\n";
		s[4] = "We found 'd' inverse of 'e' in f" + "\r\n" +
			   "d: " + d + "\r\n"; 
		s[5] = "We get C=M^e (mod n)" + "\r\n" +
			   "C: " + C + "\r\n";
		s[6] = "We can get back M from M=C^d (mod n)" + "\r\n" +
			   "M: " + M;
		return s;
	}
	
	public static String getC() {
		return C.toString();
	}
	
	public static String getDT() {
		return decryptedText;
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
	
	public static void start(String in) {
		stringPlainText = in;
		bytePlainText = in.getBytes();
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
		try {
			decryptedText = new String(byteCipherText, "US-ASCII");
		} catch (UnsupportedEncodingException e1) {
			e1.printStackTrace();
		}
		print();
	}

}
