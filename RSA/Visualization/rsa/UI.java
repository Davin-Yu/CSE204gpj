package rsa;

import java.awt.Color;
import java.awt.Font;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.IOException;

import javax.imageio.ImageIO;
import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JTextArea;
import javax.swing.JTextField; 

public class UI {
	
	private static int width = 1280;
	private static int height = 720;
	private static Font theFont = new Font("Times New Roman", Font.PLAIN, 25);
	private static Font theFont2 = new Font(null, Font.PLAIN, 14);
	private static String s[];
	private static String str = "";
	private static int step = 0;
	private static boolean started = false;

	public static void main(String[] args) {

    	JFrame frame = new JFrame("RSA Visualization");
        frame.setSize(width, height+200);
        frame.setLocation(width/10, height/10);
        frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);

        JPanel panel = new JPanel(); 
        panel.setBackground(new Color(255,255,255));
        frame.add(panel);
        placeComponents(panel);
        frame.setVisible(true);
	}

	private static void placeComponents(JPanel panel) {

        panel.setLayout(null);
        
        /* Bob */
        BufferedImage myBob = null;
		try {
			myBob = ImageIO.read(new File("Bob.png"));
		} catch (IOException e) {
			e.printStackTrace();
		}
        JLabel picLabel1 = new JLabel(new ImageIcon(myBob));
        picLabel1.setBounds(width*7/10+10, height/7, 230, 230);
        panel.add(picLabel1);
        
        JTextField BobText = new JTextField(1);
        BobText.setBounds(width*7/10-35, height/2, 300, 50);
        BobText.setHorizontalAlignment(JTextField.CENTER);
        BobText.setFont(theFont);
        BobText.setBackground(new Color(255,255,255));
        BobText.setEditable(false);
        panel.add(BobText);
        
        /* Alice */
        BufferedImage myAlice = null;
		try {
			myAlice = ImageIO.read(new File("Alice.png"));
		} catch (IOException e) {
			e.printStackTrace();
		}
        JLabel picLabel2 = new JLabel(new ImageIcon(myAlice));
        picLabel2.setBounds(width*1/11, height/7, 230, 230);
        panel.add(picLabel2);
        
        JTextField AliceText = new JTextField(1);
        AliceText.setBounds(width*1/11-35, height/2, 300, 50);
        AliceText.setHorizontalAlignment(JTextField.CENTER);
        AliceText.setFont(theFont);
        panel.add(AliceText);
        
        /* Pipe */
        JTextArea pipeText = new JTextArea(3, 10);
        pipeText.setBounds(width*1/11+230, height*1/4+50, width*7/10-width*1/11-230, 40);
        pipeText.setBackground(new Color(250,250,250));
        pipeText.setEditable(false);
        pipeText.setLineWrap(true);
        panel.add(pipeText);
        
        /* Display */
        JTextArea resName = new JTextArea(1, 5);
        resName.setBounds(width*1/11-30, height*1/2+110, 200, 30);
        resName.setText("Algorithm");
        resName.setFont(theFont);
        resName.setEditable(false);
        resName.setLineWrap(true);
        panel.add(resName);
        
        JTextArea resText = new JTextArea(14, 10);
        resText.setBounds(width*1/11-30, height*1/2+150, 1080, 300);
        resText.setBackground(new Color(250,250,250));
        resText.setFont(theFont2);
        resText.setEditable(false);
        resText.setLineWrap(true);
        panel.add(resText);
        
        
        /* Button */
        JButton nextButton = new JButton("NEXT");
        nextButton.setBounds(width-200, height+100, 80,30);
        nextButton.addActionListener(new ActionListener() {
        	@Override
        	public void actionPerformed(ActionEvent e) {
        		if ((step <6) && (started)) {
        			step++;
        			str = str + s[step];
        			resText.setText(str);
        			if (step == 5) {
        				pipeText.setText(Algorithm.getC());
        			}
        			if (step == 6) {
        				BobText.setText(Algorithm.getDT());
        			}
        		}
        	}
        });
        panel.add(nextButton);
        
        JButton startButton = new JButton("START");
        startButton.setBounds(width/2-75, height/10, 150, 40);
        startButton.setFont(theFont);
        startButton.setBackground(new Color(123,234,211));
        startButton.addActionListener(new ActionListener(){
			@Override
			public void actionPerformed(ActionEvent e) {
				if (!started) {
					started = true;
					Algorithm.start(AliceText.getText());
					s = Algorithm.print();
					str = str + s[step];
					resText.setText(str);
					AliceText.setEditable(false);
					AliceText.setBackground(new Color(255,255,255));
				}
			}
        });
        panel.add(startButton);
    }
	
}
